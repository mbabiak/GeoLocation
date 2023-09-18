import { Component, OnInit } from '@angular/core';
import { GeoApiService } from '../geo-api.service';
import { ProgressHubService } from '../progress-hub.service';
import { take, takeUntil} from 'rxjs/operators';
import { ReplaySubject, BehaviorSubject } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-geo-search',
  templateUrl: './geo-search.component.html',
  styleUrls: ['./geo-search.component.css']
})
export class GeoSearchComponent implements OnInit {
  public centerLongitude: number;
  public centerLatitude: number;
  public borderLongitude: number;
  public borderLatitude: number;
  public selectedFileName: string;
  public file: File;
  public countResult: number = 0;
  loadingSubject$ = new BehaviorSubject<boolean>(false);
  private hubConnectionId: string;
  private destroyed$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private geoApiService: GeoApiService, public signalRService: ProgressHubService, private snackBar: MatSnackBar) { }

  ngOnInit(): void { 

    this.signalRService.startConnection()
    .then(_ => this.signalRService.getConnectionId()
      .pipe(take(1))
      .subscribe(id => this.hubConnectionId = id)
    );

   this.signalRService.getCoordinatesResult()
     .pipe(takeUntil(this.destroyed$))
     .subscribe(x => this.countResult = x);
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length) {
      this.file = input.files[0];
      this.selectedFileName = input.files[0].name;
    }
  }

  onSubmit() {
    if(!this.file) {
      this.snackBar.open('No file selected!', 'Close', {
        duration: 3000
      });
    }
    else {
      this.countResult = 0;
      this.loadingSubject$.next(true);
  
      const data = {
        centerLongitude: this.centerLongitude,
        centerLatitude: this.centerLatitude,
        borderLongitude: this.borderLongitude,
        borderLatitude: this.borderLatitude,
        hubConnectionId: this.hubConnectionId
      };
  
        this.geoApiService.searchLocations(data, this.file).subscribe({
          next: () => { 
            this.snackBar.open('Success!', 'Close', {
            duration: 3000
          })},
          error: () => {
            this.loadingSubject$.next(false);
            this.snackBar.open('Error!', 'Close', {
              duration: 3000
            });
          },
          complete: () => this.loadingSubject$.next(false)
        });
    }
    }
}
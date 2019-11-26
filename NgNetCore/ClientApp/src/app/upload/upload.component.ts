import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { HandleErrorService } from '../@base/services/handle-error.service';

//Tomado de https://code-maze.com/upload-files-dot-net-core-angular/
// https://www.positronx.io/angular-file-upload-with-progress-bar-tutorial/
//https://getbootstrap.com/docs/4.3/components/progress/
//https://dev.to/codingdefined/uploading-image-from-angular-to-asp-net-core-web-api-4enm

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

    progress: number;
    message: string;
    baseUrl: string;

    @Output() public onUploadFinished = new EventEmitter();

    constructor(
        private http: HttpClient,
         @Inject('BASE_URL') baseUrl: string,
        private handleErrorService: HandleErrorService
    ) {
        this.baseUrl = baseUrl;
    }

    ngOnInit() {
    }

    public uploadFile = (files) => {
        if (files.length === 0) {
            return;
        }

        let fileToUpload = <File>files[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        //formData.append('file', fileToUpload, fileToUpload.name);

        //UploadByte
        //'https://localhost:44390/api/Upload' con  'UploadByte' or 'UploadProfilePicture' o 'UploadMultiplesProfilePicture'
        
        this.http.post(this.baseUrl+'/api/Upload/UploadByte', formData,{ reportProgress: true, observe: 'events' })
            .subscribe(event => {
                if (event.type === HttpEventType.UploadProgress)
                    this.progress = Math.round(100 * event.loaded / event.total);
                else if (event.type === HttpEventType.Response) {
                    this.message = 'Upload success.';
                    this.onUploadFinished.emit(event.body);
                }
            });
    }



}

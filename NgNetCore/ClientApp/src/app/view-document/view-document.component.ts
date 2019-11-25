import { Component, OnInit } from '@angular/core';
//import { NgxDocViewerModule } from 'ngx-doc-viewer'; // va en el module
//"ngx-doc-viewer": "^0.1.24",package json
@Component({
  selector: 'app-view-document',
  templateUrl: './view-document.component.html',
  styleUrls: ['./view-document.component.css']
})
export class ViewDocumentComponent implements OnInit {

  viewer = 'google';
  selectedType = 'docx';
  doc = 'https://file-examples.com/wp-content/uploads/2017/02/file-sample_100kB.docx';

  constructor() { }

  ngOnInit() {
  }

  setDocLocation(type: string) {
        this.selectedType = type;
        switch (type) {
            case 'docx':
                this.doc = 'https://file-examples.com/wp-content/uploads/2017/02/file-sample_100kB.docx';
                break;
            case 'xslx':
                this.doc = 'https://file-examples.com/wp-content/uploads/2017/02/file_example_XLSX_10.xlsx';
                break;
            case 'tiff':
                this.doc = 'https://file-examples.com/wp-content/uploads/2017/10/file_example_TIFF_1MB.tiff';
                break;
            case 'pdf':
                this.doc = 'https://file-examples.com/wp-content/uploads/2017/10/file-sample_150kB.pdf';
                break;
            case 'ppt':
                this.doc = 'https://file-examples.com/wp-content/uploads/2017/08/file_example_PPT_250kB.ppt';
                break;
        }
    }

}

import { Component } from '@angular/core';
import { SalesunitService } from './services/salesunit.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [SalesunitService]
})
export class AppComponent {
  title = 'ClientApp';
}

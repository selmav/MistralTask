import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { OrderingComponent } from "./ordering/ordering.component";
import { PopupComponent } from "./popup/popup.component";

@NgModule({
    declarations: [
        OrderingComponent,
        PopupComponent
    ],
    imports: [
      BrowserModule,
      CommonModule,
    ],
    exports: [
        OrderingComponent,
        PopupComponent
    ]
  })
  export class SharedModule { }
  
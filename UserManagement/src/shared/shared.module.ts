import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { OrderingComponent } from "./ordering/ordering.component";

@NgModule({
    declarations: [
        OrderingComponent
    ],
    imports: [
      BrowserModule,
      CommonModule,
    ],
    exports: [
        OrderingComponent
    ]
  })
  export class SharedModule { }
  
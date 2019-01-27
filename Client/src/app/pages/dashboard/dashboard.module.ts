import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";

import { DashboardComponent } from "./dashboard.component";
import {
  NbCardModule,
  NbInputModule,
  NbButtonModule,
  NbSelectModule
} from "@nebular/theme";
import { FormsModule } from "@angular/forms";

@NgModule({
  imports: [
    NbCardModule,
    CommonModule,
    FormsModule,
    NbInputModule,
    NbButtonModule,
    NbSelectModule
  ],
  declarations: [DashboardComponent]
})
export class DashboardModule {}

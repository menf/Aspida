import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { Ng2SmartTableModule } from "ng2-smart-table";
import { DashboardComponent } from "./dashboard.component";
import {
  NbCardModule,
  NbInputModule,
  NbButtonModule,
  NbSelectModule,
  NbTabsetModule,
  NbListModule,
  NbAccordionModule
} from "@nebular/theme";
import { FormsModule } from "@angular/forms";

@NgModule({
  imports: [
    NbCardModule,
    CommonModule,
    FormsModule,
    NbInputModule,
    NbButtonModule,
    NbSelectModule,
    NbTabsetModule,
    NbListModule,
    NbAccordionModule,
    Ng2SmartTableModule
  ],
  declarations: [DashboardComponent]
})
export class DashboardModule {}

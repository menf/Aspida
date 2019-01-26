import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";

import { DashboardComponent } from "./dashboard.component";
import { NbCardModule } from "@nebular/theme";

@NgModule({
  imports: [NbCardModule, CommonModule],
  declarations: [DashboardComponent]
})
export class DashboardModule {}

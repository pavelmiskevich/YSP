import { Injectable } from "@angular/core";
import { Region } from './region.model';
import { RestDataSource } from './rest.datasource';
import { StaticDataSource } from "./static.datasource";
@Injectable()
export class RegionRepository {
    public regions: Region[] = [];
    constructor(private dataSource: StaticDataSource) {
        dataSource.getRegions().subscribe(data => {
            data.splice(0, 1); //регионы
            data.splice(29, 1); //----------
            this.regions = data; //.reverse(); //.sort((a, b) => (b.id - a.id));            
        });
    }

    getRegions(): Region[] {
        return this.regions;
    }
}
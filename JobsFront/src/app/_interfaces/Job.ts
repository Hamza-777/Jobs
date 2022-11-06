import { Category } from "./Category";
import { City } from "./City";
import { State } from "./State";

  export interface Job {
    id: number;
    description: string;
    redirect_url: string;
    salary_max: number;
    location: string;
    title: string;
    salary_min: number;
    company: string;
    created: string;
    state: State;
    stateid: number;
    city: City;
    cityid: number;
    category: Category;
    categoryid: number;
    userid: number;
    
}
  
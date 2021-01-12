export interface IEntity {   
    type: string;
    id?: number;
    addDate?: string;
    isActive?: boolean;

    getName() : string;
  }
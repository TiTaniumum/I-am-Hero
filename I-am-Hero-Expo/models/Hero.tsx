export interface IHero{
    id: number,
    name: string,
    experience: number,
    cLevelCalculationTypeId: number
}

export class Hero implements IHero {
    id: number;
    name: string;
    experience: number;
    cLevelCalculationTypeId: number;
    constructor(hero:IHero){
        this.id = hero.id
        this.name = hero.name
        this.experience = hero.experience
        this.cLevelCalculationTypeId = hero.cLevelCalculationTypeId
    }
}
export interface ISkill {
  id: number;
  name: string;
  description: string;
  experience: number;
  cLevelCalculationTypeId: number;
}

export class Skill implements ISkill {
    id: number;
    name: string;
    description: string;
    experience: number;
    cLevelCalculationTypeId: number;

    constructor(skill: ISkill){
        this.id = skill.id;
        this.name = skill.name;
        this.description = skill.description;
        this.experience = skill.experience;
        this.cLevelCalculationTypeId = skill.cLevelCalculationTypeId;
    }

    static AcceptArr(skills: ISkill[]): Skill[]{
        const arr: Skill[] = [];
        skills.forEach(i=>{
            arr.push(new Skill(i));
        })
        return arr;
    }
}

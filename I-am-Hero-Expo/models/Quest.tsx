
export interface IBehaviour {
  id?: number;
  heroAttirbuteId: number | null;
  heroSkillId: number | null;
  sign: string;
  value: number;
}

export interface IQuestDto{
    id?: number | null;
    title: string | null;
    description: string | null;
    experience: number | null;
    completionBehaviour: IBehaviour | null;
    failureBehaviour: IBehaviour | null;
    priority: number | null;
    cDifficultyId: number | null;
    cQuestStatusId: number | null;
    questLineId: number | null;
    deadline: Date | null;
    createDate: Date | null;
    archiveDate: Date | null;  
}

export interface IQuest {
  id: number;
  title: string;
  description: string | null;
  experience: number;
  completionBehaviour: IBehaviour | null;
  failureBehaviour: IBehaviour | null;
  priority: number | null;
  cDifficultyId: number | null;
  cQuestStatusId: number | null;
  questLineId: number | null;
  deadline: Date | null;
  createDate: Date | null;
  archiveDate: Date | null;
}

export class Quest implements IQuest {
  id: number;
  title: string;
  description: string | null;
  experience: number;
  completionBehaviour: IBehaviour | null;
  failureBehaviour: IBehaviour | null;
  priority: number | null;
  cDifficultyId: number | null;
  cQuestStatusId: number | null;
  questLineId: number | null;
  deadline: Date | null;
  createDate: Date | null;
  archiveDate: Date | null;

  constructor(quest: IQuest) {
    this.id = quest.id;
    this.title = quest.title;
    this.description = quest.description;
    this.experience = quest.experience;
    this.completionBehaviour = quest.completionBehaviour;
    this.failureBehaviour = quest.failureBehaviour;
    this.priority = quest.priority;
    this.cDifficultyId = quest.cDifficultyId;
    this.cQuestStatusId = quest.cQuestStatusId;
    this.questLineId = quest.questLineId;
    this.deadline = quest.deadline;
    this.createDate = quest.createDate;
    this.archiveDate = quest.archiveDate;
  }

  static AcceptArr(quests: IQuest[]): Quest[]{
    const arr: Quest[] = [];
    quests.forEach((item)=>{
        arr.push(new Quest(item));
    })
    return arr;
  }
}

export interface IQuestStatus {
  id: number;
  nameEn: string;
  nameRu: string;
}

export default class QuestStatus implements IQuestStatus {
  id: number;
  nameEn: string;
  nameRu: string;

  constructor(questStatus: IQuestStatus) {
    this.id = questStatus.id;
    this.nameEn = questStatus.nameEn;
    this.nameRu = questStatus.nameRu;
  }

  static AcceptArr(questStatuses: IQuestStatus[]): QuestStatus[] {
    const arr: QuestStatus[] = [];
    questStatuses.forEach((i) => {
      arr.push(new QuestStatus(i));
    });
    return arr;
  }
}

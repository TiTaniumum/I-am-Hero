export interface IBioPiece {
  id: number;
  text: string;
  createDate: Date | string;
}

export class BioPiece implements IBioPiece {
  id: number;
  text: string;
  createDate: Date;
  
  constructor(hero: IBioPiece) {
    this.id = hero.id;
    this.text = hero.text;
    if (hero.createDate instanceof Date) this.createDate = hero.createDate;
    else this.createDate = new Date(hero.createDate);
  }

  static AcceptArr(pieces: IBioPiece[]): BioPiece[] {
    const arr: BioPiece[] = [];
    pieces.forEach((i) => {
      arr.push(new BioPiece(i));
    });
    return arr;
  }
}

import AsyncStorage from "@react-native-async-storage/async-storage";
import { Hero, IHero } from "./Hero";
import { BioPiece } from "./BioPiece";
import { Attribute, AttributeState } from "./Attribute";
import { Skill } from "./Skill";
import { Quest } from "./Quest";

export default class User {
  static C: number = 50;

  hero?: Hero;
  skills: Skill[] = [];
  attributes: Attribute[] = [];
  attributeStates: AttributeState[] = [];
  statuseffects?: any;
  biopieces: BioPiece[] = [];
  achievements?: any;
  quests: Quest[] = [];
  questlines?: any;
  behaviours?: any;
  calendars?: any;
  habbits?: any;

  setIsHero?: any;
  async Init() {
    const user = this;
    await AsyncStorage.getItem("Hero").then((str) => {
      if (str) {
        user.hero = new Hero(JSON.parse(str));
        user.setIsHero(true);
      }
    });
  }

  static GetLevel(xp: number, calcType: number) {
    switch (calcType) {
      case 1:
      default:
        return Math.floor((-1 + Math.sqrt(1 + (8 * xp) / this.C)) / 2);
    }
  }

  static GetExp(lvl: number, calcType: number) {
    switch (calcType) {
      case 1:
      default:
        return (this.C * (lvl * (lvl + 1))) / 2;
    }
  }

  static GetExpPerLevel(
    xp: number,
    calcType: number
  ): { curXp: number; xpToNextLvl: number } {
    switch (calcType) {
      case 1:
      default:
        const lvl = this.GetLevel(xp, calcType);
        const xpOnLvl = this.GetExp(lvl, calcType);
        const curXp = xp - xpOnLvl;
        const xpToNextLvl = (lvl + 1) * this.C;
        return { curXp, xpToNextLvl };
    }
  }
}

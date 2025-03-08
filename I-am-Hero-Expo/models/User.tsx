import AsyncStorage from "@react-native-async-storage/async-storage";
import { Hero, IHero } from "./Hero";
import { BioPiece } from "./BioPiece";
import { Attribute, AttributeState } from "./Attribute";

export default class User {
  hero?: Hero;
  skills?: any;
  attributes?: Attribute[];
  attributeStates?: AttributeState[];
  statuseffects?: any;
  biopieces?: BioPiece[];
  achievements?: any;
  quests?: any;
  questlines?: any;
  behaviours?: any;
  calendars?: any;
  habbits?: any;
  
  setIsHero?: any;
  async Init() {
    const user = this;
    await AsyncStorage.getItem("Hero").then((str) => {
      if (str){
        user.hero = new Hero(JSON.parse(str));
        user.setIsHero(true);
      }
    });
  }
}

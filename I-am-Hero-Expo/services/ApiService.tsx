import Resource from "@/constants/Resource";
import { Attribute, AttributeState, IAttributeDTO } from "@/models/Attribute";
import { BioPiece } from "@/models/BioPiece";
import Common from "@/models/Common";
import { Hero } from "@/models/Hero";
import { IQuestDto, Quest } from "@/models/Quest";
import QuestStatus from "@/models/QuestStatus";
import { ISkillDTO, Skill } from "@/models/Skill";
import User from "@/models/User";
import AsyncStorage from "@react-native-async-storage/async-storage";
import axios, { AxiosInstance, AxiosResponse } from "axios";
import { Platform } from "react-native";

export default class ApiService {
  private baseUrl: string = "http://192.168.1.68:8080/api/";
  private applicationID: number;
  token: string | null = null;
  alert: (title: string, message: string) => void = (
    title: string,
    message: string
  ) => {};
  setIsToken: any = (value: any) => {};
  user: User;
  common: Common;
  constructor(user: User, common: Common) {
    this.user = user;
    this.common = common;
    switch (Platform.OS) {
      case "android":
        this.applicationID = 3;
        break;
      case "ios":
        this.applicationID = 4;
        break;
      case "web":
      default:
        this.applicationID = 1;
        break;
    }
  }

  async GetToken() {
    const value: string | null = await AsyncStorage.getItem("authToken");
    if (value != null) {
      this.token = value;
      this.setIsToken(true);
    }
  }

  Login(email: string, password: string) {
    const api = this;
    return axios
      .post(this.uri("Auth/login"), {
        Email: email,
        Password: password,
        ApplicationId: this.applicationID,
      })
      .then(function (response) {
        api.alert(Resource.get("success!"), "");
        api.SetNewToken(response.data);
        api.GetHero(api.user);
      })
      .catch(function (error) {
        console.log(error);
        if (error.status == 400) {
          api.alert(Resource.get("error!"), Resource.get("errorUserNotExist"));
        }
      });
  }

  Logout() {
    this.token = null;
    AsyncStorage.removeItem("authToken");
    if (this.user != null && this.user.hero != undefined)
      this.user.hero = undefined;
    AsyncStorage.removeItem("Hero");
    this.setIsToken(false);
    this.user.setIsHero(false);
  }

  Register(
    email: string,
    password: string,
    passwordRepeat: string,
    setIsLogin: any
  ) {
    const alert = this.alert;
    if (password == passwordRepeat)
      axios
        .post(this.uri("Auth/register"), {
          email: email,
          password: password,
        })
        .then(function (response) {
          alert(Resource.get("success!"), "");
          setIsLogin(true);
        })
        .catch(function (error) {
          console.log(error);
        });
    else alert(Resource.get("warning!"), Resource.get("passwordRepeatWarn"));
  }

  CreateHero(name: string, user: User) {
    const api = this;
    axios
      .post(
        this.uri(`Hero/create?heroName=${name}`),
        {},
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      )
      .then(function (response) {
        const j = api.handleToken(response);
        api.GetHero(user);
      })
      .catch(function (error) {
        api.alert(Resource.get("error!"), Resource.get("somethingwrong"));
        console.log(error);
      });
  }

  GetHero(user: User) {
    const api = this;
    return axios
      .get(this.uri("Hero/get"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response)=>{
        const j = api.handleToken(response);
        user.hero = new Hero(j);
        AsyncStorage.setItem("Hero", JSON.stringify(user.hero));
        user.setIsHero(true);
      }).catch(()=>{});
  }

  EditHeroExp(xp: number){
    const api = this;
    const user = this.user;
    return axios
    .put(
      this.uri("Hero/edit-heroExperience"),
      {},
      {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
        params: {
          exp: xp
        }
      }
    )
    .then(api.handleToken);
  }

  GetBioPieces() {
    const api = this;
    const user = this.user;
    return axios
      .get(this.uri("Hero/get/HeroBioPieces"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(function (response) {
        const j = api.handleToken(response);
        user.biopieces = BioPiece.AcceptArr(j.heroBioPieces);
      });
  }

  CreateBioPiece(text: string) {
    const api = this;
    return axios
      .post(
        this.uri("Hero/create/HeroBioPiece"),
        { text: text },
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      )
      .then(api.handleToken);
  }

  DeleteBioPiece(id: number) {
    const api = this;
    return axios
      .delete(this.uri("Hero/delete/HeroBioPiece"), {
        params: { id: id },
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  EditBioPiece(id: number, text: string) {
    const api = this;
    return axios
      .put(
        this.uri("Hero/edit/HeroBioPiece"),
        { id: id, text: text },
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      )
      .then(api.handleToken);
  }

  CreateAttribute(attribute: IAttributeDTO) {
    const api = this;
    return axios
      .post(this.uri("Hero/create/HeroAtrribute"), attribute, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  EditAttribute(attribute: IAttributeDTO) {
    const api = this;
    return axios
      .put(this.uri("Hero/edit/HeroAttribute"), attribute, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  CreateAttributeStates(
    attributeStates: { heroAttributeId: number; name: string }[]
  ) {
    const api = this;
    return axios
      .post(
        this.uri("Hero/create/HeroAttributeStates"),
        { heroAttributeStates: attributeStates },
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      )
      .then(api.handleToken);
  }

  GetAttributes() {
    const api = this;
    const user = this.user;
    return axios
      .get(this.uri("Hero/get/HeroAttributes"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        const j = api.handleToken(response);
        user.attributes = Attribute.AcceptArr(j.heroAttributes);
      });
  }

  DeleteAttributeStates(ids: number[]) {
    const api = this;
    ids.forEach((id) => {
      axios
        .delete(this.uri("Hero/delete/HeroAttributeState"), {
          params: { id: id },
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        })
        .then(api.handleToken)
        .catch((error) => api.alert("ERROR", "Something went wrong!"));
    });
  }

  PullAttributeStates() {
    const user = this.user;
    user.attributes?.forEach(async (attribute) => {
      // 2 - тип состояние; 1 - численный
      if (attribute.cAttributeTypeId == 1) return;
      const response = await axios.get(
        this.uri(
          `Hero/get/HeroAttributeStates?heroAttributeId=${attribute.id}`
        ),
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      );
      attribute.states = AttributeState.AcceptArr(
        response.data.heroAttributeStates
      );
    });
  }

  GetAttributeStates() {
    const api = this;
    const user = this.user;
    return axios
      .get(this.uri("Hero/get/HeroAttributeStates"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        const j = api.handleToken(response);
        user.attributeStates = AttributeState.AcceptArr(
          response.data.heroAttributeStates
        );
      });
  }

  DeleteAttribute(id: number) {
    const api = this;
    return axios
      .delete(this.uri("Hero/delete/HeroAttribute"), {
        params: { id: id },
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  GetSkills() {
    const api = this;
    const user = this.user;
    return axios
      .get(this.uri("Hero/get/HeroSkills"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        const j = api.handleToken(response);
        user.skills = Skill.AcceptArr(j.heroSkills);
      });
  }

  CreateSkill(skill: ISkillDTO) {
    const api = this;
    return axios
      .post(this.uri("Hero/create/HeroSkill"), skill, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  EditSkill(skill: ISkillDTO) {
    const api = this;
    return axios
      .put(this.uri("Hero/edit/HeroSkill"), skill, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  DeleteSkill(id: number) {
    const api = this;
    return axios
      .delete(this.uri("Hero/delete/HeroSkill"), {
        params: { id: id },
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  GetQuests() {
    const api = this;
    const user = this.user;
    return axios
      .get(this.uri("Hero/get/Quests"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then((response) => {
        const j = api.handleToken(response);
        user.quests = Quest.AcceptArr(j.quests);
      });
  }

  CreateQuest(quest: IQuestDto) {
    const api = this;
    return axios
      .post(this.uri("Hero/create/Quest"), quest, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  EditQuest(quest: IQuestDto) {
    const api = this;
    return axios
      .put(this.uri("Hero/edit/Quest"), quest, {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(api.handleToken);
  }

  DeleteQuest(id: number){
    const api = this;
    return axios.delete(this.uri("Hero/delete/Quest"), {
      params: { id: id },
      headers: {
        Authorization: `Bearer ${this.token}`,
      },
    })
    .then(api.handleToken);
  }

  GetcQuestStatuses() {
    const api = this;
    const common = this.common;
    return axios.get(this.uri("Common/all-cQuestStatus")).then((response) => {
      const j = api.handleToken(response);
      common.questStatuses = QuestStatus.AcceptArr(j);
    });
  }

  private handleToken(response: AxiosResponse<any, any>) {
    const j = response.data;
    if (j.token != "" && j.token != null && j.token != undefined) {
      this.SetNewToken(j.token);
    }
    return j;
  }
  private uri(endPart: string) {
    return `${this.baseUrl}${endPart}`;
  }
  private SetNewToken(token: string) {
    if (token === "") return;
    AsyncStorage.setItem("authToken", token);
    this.token = token;
    this.setIsToken(true);
  }
  private handleException(error: any) {}
}

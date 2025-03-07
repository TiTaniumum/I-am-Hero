import Resource from "@/constants/Resource";
import { Attribute, AttributeState } from "@/models/Attribute";
import { BioPiece } from "@/models/BioPiece";
import { Hero } from "@/models/Hero";
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
  constructor(user: User) {
    this.user = user;
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
    axios
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
        api.alert("error", error.response?.data?.message || error.message);
        console.log(error);
      });
  }
  GetHero(user: User) {
    if (this.token == null) return;
    const api = this;
    axios
      .get(this.uri("Hero/get"), {
        headers: {
          Authorization: `Bearer ${this.token}`,
        },
      })
      .then(function (response) {
        const j = api.handleToken(response);
        user.hero = new Hero(j);
        AsyncStorage.setItem("Hero", JSON.stringify(user.hero));
        user.setIsHero(true);
      })
      .catch((error) => console.log(error));
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
        api.PullAttributeStates();
      });
  }

  PullAttributeStates() {
    const api = this;
    const user = this.user;
    user.attributes?.forEach(async (attribute) => {
      // 2 - тип состояние; 1 - численный
      if (
        attribute.currentStateID === undefined ||
        attribute.currentStateID instanceof AttributeState ||
        attribute.currentStateID == 1
      )
        return;
      const response = await axios.get(
        this.uri(`Hero/get/HeroAttributeStates?id=${attribute.currentStateID}`)
      );
      const attributeState = new AttributeState(response.data);
      attribute.currentStateID = attributeState;
    });
  }

  private handleToken(response: AxiosResponse<any, any>) {
    const j = response.data;
    if (j.token != "" && j.token != null) {
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

import Resource from "@/constants/Resource";
import AsyncStorage from "@react-native-async-storage/async-storage";
import axios from "axios";
import { Platform } from "react-native";

export default class ApiService {
  private baseUrl: string = "http://192.168.1.65:8080/api/";
  private applicationID: number;
  token: string | null = null;
  alert: (title: string, message: string) => void = (title: string, message: string)=>{};
  setIsToken: any = (value:any)=>{};
  constructor() {
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
    if(value){
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
        api.alert(Resource.get("success!"),"");
        api.SetNewToken(response.data);
      })
      .catch(function (error) {
        console.log(error);
        if(error.status == 400){
            api.alert(Resource.get("error!"),Resource.get("errorUserNotExist"));
        }
      });
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
          alert(Resource.get("success!"),"");
          setIsLogin(true);
        })
        .catch(function (error) {
          console.log(error);
        });
    else alert(Resource.get("warning!"),Resource.get("passwordRepeatWarn"));
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
}

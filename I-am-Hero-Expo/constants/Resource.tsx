const source: { [key: string]: { [loc: string]: string } } = {
  "error!": { ru: "Ошибка!", en: "Error!" },
  errorUserNotExist: {
    ru: "Такого пользователя не существует или неверный пороль.",
    en: "Such user does not exist or wrong password.",
  },
  "success!": { ru: "Успешно!", en: "Success!" },
  "warning!": { ru: "Предупреждение!", en: "Warning!" },
  passwordRepeatWarn: {
    ru: "Пороли должны совпадать.",
    en: "Passwords have to be the same.",
  },
  notAuth: { ru: "Вы не авторизованы", en: "You are not authorized" },
  login: { ru: "Логин", en: "Login" },
  noAccount: { ru: "Нету аккаунта?", en: "Don't have an account?" },
  register: { ru: "Зарегестрироваться", en: "Register" },
  registration: { ru: "Регистрация", en: "Registration" },
  alreadyAccount: { ru: "Уже есть аккаунт?", en: "Already have an account?" },
  //   "": { ru: "", en: "" },
  //   "": { ru: "", en: "" },
  //   "": { ru: "", en: "" },
  //   "": { ru: "", en: "" },
  //   "": { ru: "", en: "" },
};

export default class Resource {
  static loc: string = "ru";
  static get(key: string) {
    return source[key][Resource.loc];
  }
}

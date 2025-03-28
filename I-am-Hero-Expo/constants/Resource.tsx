const source: { [key: string]: { [loc: string]: string } } = {
  ru: { ru: "Русский", en: "Russian" },
  en: { ru: "Английский", en: "English" },
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
  logout: { ru: "Разлогиниться", en: "Logout" },
  iam: { ru: "Я", en: "I am" },
  level: { ru: "Уровень", en: "Level" },
  diary: { ru: "Дневник", en: "Diary" },
  "": { ru: "ПУСТАЯ СТРОКА", en: "EMPTY STRING" },
  attributes: { ru: "Атрибуты", en: "Attributes" },
  skills: { ru: "Скилы", en: "Skills" },
  habits: { ru: "Привычки", en: "Habits" },
  statuseffects: { ru: "Статус эффекты", en: "Status effects" },
  achievements: { ru: "Достижения", en: "Achievements" },
  daylies: { ru: "Ежедневники", en: "Dailies" },
  quests: { ru: "Квесты", en: "Quests" },
  social: { ru: "Социалка", en: "Social" },
  welcomehero: { ru: "Добро пожаловать, Герой!", en: "Wellcome, Hero!" },
  "whatname?": { ru: "Как вас зовут?", en: "What is your name?" },
  create: { ru: "Создать", en: "Create" },
  somethingwrong: { ru: "Что-то пошло не так", en: "Something went wrong..." },
  edit: { ru: "Изменить", en: "Edit" },
  delete: { ru: "Удалить", en: "Delete" },
  cancel: { ru: "Отменить", en: "Cancel" },
  "deleted!": { ru: "Удалено!", en: "Deleted!" },
  washero: { ru: "Жил да был герой", en: "There once was a hero" },
  named: { ru: "по имени", en: "named" },
  "created!": { ru: "Создано!", en: "Created!" },
  typehere: { ru: "пишите сюда...", en: "type here..." },
  erase: { ru: "Стереть", en: "Erase" },
  "saved!": { ru: "Сохранено", en: "Saved!" },
  rollback: { ru: "Откатиться", en: "Rollback" },
  save: { ru: "Сохранить", en: "Save" },
  noattributes: {
    ru: "У вас пока что нету атрибутов",
    en: "You have no attributes yet",
  },
  minbigmax: {
    ru: "Минимально значение не может быть больше максимального",
    en: "Minimum value cannot be bigger than maximum",
  },
  curbigmax: {
    ru: "Текущее значение не может быть больше максимального",
    en: "Current value cannot be bigger than maximum",
  },
  curlesmin: {
    ru: "Текущее значние не может быть меньше минимального",
    en: "Current value cannot be lesser than minimum",
  },
  attributename: { ru: "Название атрибута...", en: "Attribute name..." },
  description: { ru: "Описание...", en: "Description..." },
  numeric: { ru: "Численное", en: "Numeric" },
  state: { ru: "Состояние", en: "State" },
  min: { ru: "мин", en: "min" },
  cur: { ru: "тек", en: "cur" },
  max: { ru: "макс", en: "max" },
  statename: { ru: "Состояние...", en: "State name..." },
  curstate: { ru: "Текущее состояние", en: "Current state: " },
  createattribute: { ru: "Создание атрибута", en: "Attribute creation" },
  createbiography: {
    ru: "Создание записи дневника",
    en: "Diary note creation",
  },
  editattribute: { ru: "Изменение атрибута", en: "Attribute edit" },
  editbiography: { ru: "Изменение записи дневника", en: "Diary note edit" },
  habbits: { ru: "Привычки", en: "Habbits" },
  settings: { ru: "Настройки", en: "Settings" },
  language: { ru: "Язык", en: "Language" },
  createskill: { ru: "Создание скила", en: "Skill creation" },
  skillname: { ru: "Назавние скила...", en: "Skill name..." },
  expamount: {
    ru: "Количество опыта для скила",
    en: "Amount of experience for the skill...",
  },
  xp: { ru: "опыт", en: "xp" },
  noskillsyet: {
    ru: "Вы пока что не создавали скилы",
    en: "You didn't create skills yet",
  },
  editskill: { ru: "Изменение скила", en: "Skill edit" },
  title: { ru: "Заголовок...", en: "Title..." },
  xpquest: { ru: "Опыт за выполнение...", en: "Experience for completion..." },
  experience: { ru: "Опыт", en: "Experience" },
  simple: { ru: "Простой", en: "Simple" },
  detailed: { ru: "Детальный", en: "Detailed" },
  editquest: { ru: "Изменение квеста", en: "Quest edit" },
  createquest: { ru: "Создание квеста", en: "Quest creation" },
  //   "": { ru: "", en: "" },
};

export default class Resource {
  static localizations: string[] = ["ru", "en"];
  static loc: string = "ru";
  static get(key: string) {
    return source[key][Resource.loc];
  }
  static extract(item: { nameEn: string; nameRu: string }): string {
    if (Resource.loc == "en") return item.nameEn;
    else return item.nameRu;
  }
}

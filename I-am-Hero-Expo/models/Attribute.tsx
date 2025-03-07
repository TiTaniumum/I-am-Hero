export interface IAttributeState {
  id: number;
  heroAttributeId: number;
  name: string;
}

export class AttributeState implements IAttributeState {
  id: number;
  heroAttributeId: number;
  name: string;

  constructor(attributeState: IAttributeState) {
    this.id = attributeState.id;
    this.heroAttributeId = attributeState.heroAttributeId;
    this.name = attributeState.name;
  }

  static AcceptArr(attributes: IAttributeState[]): AttributeState[] {
    const arr: AttributeState[] = [];
    attributes.forEach((i) => {
      arr.push(new AttributeState(i));
    });
    return arr;
  }
}

export interface IAttribute {
  id: number;
  name: string;
  description: string | null ;
  cAttributeTypeID: number | null;
  minValue: number | null;
  value: number | null;
  maxValue: number | null;
  currentStateID: number | AttributeState | null;
}

export class Attribute implements IAttribute {
  id: number;
  name: string;
  description: string | null;
  cAttributeTypeID: number | null;
  minValue: number | null;
  value: number | null;
  maxValue: number | null;
  currentStateID: number | AttributeState | null;

  constructor(attribute: IAttribute) {
    this.id = attribute.id;
    this.name = attribute.name;
    this.description = attribute.description;
    this.cAttributeTypeID = attribute.cAttributeTypeID;
    this.minValue = attribute.minValue;
    this.value = attribute.value;
    this.maxValue = attribute.maxValue;
    this.currentStateID = attribute.currentStateID;
  }

  static AcceptArr(attributes: IAttribute[]): Attribute[] {
    const arr: Attribute[] = [];
    attributes.forEach((i) => {
      arr.push(new Attribute(i));
    });
    return arr;
  }
}

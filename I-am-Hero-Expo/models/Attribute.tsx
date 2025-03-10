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
  cAttributeTypeId: number | null;
  minValue: number | null;
  value: number | null;
  maxValue: number | null;
  currentStateId: number | null;
  states: AttributeState[] | undefined;
}

export interface IAttributeDTO{
  id?: number;
  name: string;
  description: string | null;
  cAttributeTypeId: number | null;
  minValue: number | null;
  value: number | null;
  maxValue: number | null;
  currentStateId: number | null;
}

export class Attribute implements IAttribute {
  id: number;
  name: string;
  description: string | null;
  cAttributeTypeId: number | null;
  minValue: number | null;
  value: number | null;
  maxValue: number | null;
  currentStateId: number | null;
  states: AttributeState[] | undefined;

  constructor(attribute: IAttribute) {
    this.id = attribute.id;
    this.name = attribute.name;
    this.description = attribute.description;
    this.cAttributeTypeId = attribute.cAttributeTypeId;
    this.minValue = attribute.minValue;
    this.value = attribute.value;
    this.maxValue = attribute.maxValue;
    this.currentStateId = attribute.currentStateId;
    this.states = attribute.states;
  }

  static AcceptArr(attributes: IAttribute[]): Attribute[] {
    const arr: Attribute[] = [];
    attributes.forEach((i) => {
      arr.push(new Attribute(i));
    });
    return arr;
  }
}

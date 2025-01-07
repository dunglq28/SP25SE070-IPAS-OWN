import { Field } from "./FieldForm";

export interface UserForm {
  fullName: Field<string>;
  userName: Field<string>;
  phoneNumber: Field<string>;
  DOB: Field<Date | null>;
  gender: Field<string>;
  isActive: Field<number | null>;
}

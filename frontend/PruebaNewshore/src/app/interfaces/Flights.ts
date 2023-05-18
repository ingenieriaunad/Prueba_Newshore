export interface Trip {
  origin?:      string;
  destination?: string;
  price?:       number;
}
export interface JourneyData extends Trip{
  journey?:    Journey;
}
export interface Journey extends Trip{
  flights?:    Flight[];
}
export interface Flight extends Trip {
  transport?:  Transport;
}
export interface Transport {
  flightCarrier?: string;
  flightNumber?:  string;
}
export interface FormSearch {
  origin?:      string;
  destination?: string;
  typeFlight?:  number;
  message?:     string;
}

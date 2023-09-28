export interface Movie {

    id? : number;
    title : string;
    description : string;
    category : string;
    photoURL : string;
    state : string;
    rent_date? : Date;
    userid? : number;
    usernameRented? : string;

}

export interface RentHistory {
    movie_id : number;
    user_id : number;

    rentDate : string;
    returnDate : string | null;
}

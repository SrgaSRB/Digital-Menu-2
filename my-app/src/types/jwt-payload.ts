export interface JwtPayload {
    UserId: string;
    Username: string;
    IsGlobalAdmin: string;
    LocalId: string;
    exp: number;
    iat: number;
}

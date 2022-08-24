import axios from "axios";
import { nanoid } from "nanoid";
import { SHA256, enc } from "crypto-js";
const API_URL = "https://localhost:5005/connect/authorize";

const oidcConfig = {
  onSignIn: async (user) => {
    alert("You just signed in, congratz! Check out the console!");
    console.log(user);
    window.location.hash = "";
  },
  domain: "https://localhost:5005",
  params: {
    client_id: "interactive",
    response_type: "code",
    response_mode: "query",
    redirect_uri: `${window.location.origin}/signin-callback`,
    scope: "openid email profile PlaylistsAPI",
    /*nonce: randomString(20),*/
    code_challenge_method: "S256",
  },
};

function base64URLEncode(str) {
  return str
    .toString(enc.Base64)
    .replace(/\+/g, "-")
    .replace(/\//g, "_")
    .replace(/=/g, "");
}

function generateCodeChallenge(verifier) {
  return base64URLEncode(SHA256(verifier));
}

class AuthService {
  constructor() {
    var code_verifier = localStorage.getItem("code_verifier") || nanoid(128);
    var code_challenge =
      localStorage.getItem("code_challenge") ||
      generateCodeChallenge(code_verifier);

    this.challenge = { code_challenge, code_verifier };
    this.code = localStorage.getItem("code") || nanoid(128);

    localStorage.setItem("code_challenge", this.challenge.code_challenge);
    localStorage.setItem("code_verifier", this.challenge.code_verifier);

    console.log("Challenge", this.challenge);

    this.isLoggedIn = JSON.parse(localStorage.getItem("isLoggedIn")) || false;
    localStorage.setItem("isLoggedIn", this.isLoggedIn);

    this.isLoading = JSON.parse(localStorage.getItem("isLoading")) || false;
    localStorage.setItem("isLoading", this.isLoading);

    this.finishedLoading = () => console.log("No callback hooked.");
  }

  async getUserData() {
    let token = await this.getAccessToken();
    console.log(`Retrieved token:`, token);
    return await axios
      .get(`${oidcConfig.domain}/connect/userinfo`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((response) => {
        console.log("Userdata:", response.data);
        localStorage.setItem("user", JSON.stringify(response.data));
        debugger;
        this.isLoading = false;
        localStorage.setItem("isLoading", this.isLoading);

        this.finishedLoading();

        return response.data;
      });
  }

  authorize() {
    var params = new URLSearchParams({
      ...oidcConfig.params,
      code_challenge: this.challenge.code_challenge,
    });
    var url = `${oidcConfig.domain}/connect/authorize?${params}`;
    console.log("params:", params);
    console.log("url:", url);
    window.location.href = url;
  }

  confirmRedirect(params) {
    let { code /*, scope, state, session_state*/ } = params;
    this.code = code;
    localStorage.setItem("code", code);

    this.isLoggedIn = true;
    localStorage.setItem("isLoggedIn", this.isLoggedIn);
  }

  async getAccessToken() {
    let token = localStorage.getItem("access_token");
    if (!token) {
      token = await this.getNewAccessToken();
      localStorage.setItem("access_token", token);
    }
    return token;
  }
  getNewAccessToken() {
    let code = this.code;
    let body = {
      client_id: oidcConfig.params.client_id,
      grant_type: "authorization_code",
      redirect_uri: oidcConfig.params.redirect_uri,
      scope: "PlaylistsAPI",
      client_secret: "ClientSecret1",
      code,
      code_verifier: this.challenge.code_verifier,
      code_challenge: this.challenge.code_challenge,
    };
    return axios
      .post("https://localhost:5005/connect/token", new URLSearchParams(body), {
        headers: {
          "content-type": "application/x-www-form-urlencoded",
        },
      })
      .then((response) => {
        console.log("Token data:", response.data);
        return response.data.access_token;
      });
  }

  login() {
    this.isLoading = true;
    localStorage.setItem("isLoading", this.isLoading);
    this.authorize();
  }

  logout() {
    localStorage.removeItem("code_challenge");
    localStorage.removeItem("code_verifier");
    localStorage.removeItem("isLoggedIn");
    localStorage.removeItem("user");
    localStorage.removeItem("code");
    localStorage.removeItem("access_token");
  }

  register(username, email, password) {
    return axios.post(API_URL + "signup", {
      username,
      email,
      password,
    });
  }

  get user() {
    return JSON.parse(localStorage.getItem("user"));
  }
}
export default new AuthService();

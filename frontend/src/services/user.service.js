import axios from "axios";
import AuthService from "./auth.service";

const API_USERS_URL = "/gateway/Users";

class UserService {
  async getUserInfo(permalink) {
    let token = await AuthService.getAccessToken();
    return axios
      .get(`${API_USERS_URL}/permalink/${permalink}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then(
        (response) => response.data,
        (error) => console.log(error)
      );
  }
}
export default new UserService();

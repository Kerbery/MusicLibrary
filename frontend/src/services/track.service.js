import axios from "axios";
import AuthService from "./auth.service";
const API_URL = "/gateway/Tracks";
class TrackService {
  async getTrackInfo(permalink) {
    let token = await AuthService.getAccessToken();
    return axios.get(`${API_URL}/permalink/${permalink}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
}
export default new TrackService();

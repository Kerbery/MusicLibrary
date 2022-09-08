import axios from "axios";
import AuthService from "./auth.service";
import { getTrackData } from "./mapping.helpers";

const API_TRACKS_URL = "/gateway/Tracks";

class TrackService {
  async getTrackInfo(permalink) {
    let token = await AuthService.getAccessToken();
    return axios.get(`${API_TRACKS_URL}/permalink/${permalink}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
  async getTracks(userPermalink, PageNumber = 1, PageSize = 24) {
    let token = await AuthService.getAccessToken();
    let params = new URLSearchParams({
      userPermalink: userPermalink,
      PageNumber,
      PageSize,
    });
    return axios
      .get(`${API_TRACKS_URL}?${params}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((response) => {
        //let paging = JSON.parse(response.headers["x-pagination"] || {});
        let paging = { HasNext: false };
        let items = response.data.map((track) => getTrackData(track));
        return { items, paging };
      });
  }
}
export default new TrackService();

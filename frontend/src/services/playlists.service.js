import axios from "axios";
import AuthService from "./auth.service";
const API_URL = "/gateway/Playlists";
class PlaylistService {
  async getAllPlaylists() {
    let token = await AuthService.getAccessToken();
    return axios.get(API_URL, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
  async getPlaylistContent(playlistId) {
    let token = await AuthService.getAccessToken();
    return axios.get(API_URL + `/${playlistId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
}
export default new PlaylistService();

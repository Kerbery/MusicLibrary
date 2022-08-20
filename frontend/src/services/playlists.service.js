import axios from "axios";
import authHeader from "./auth-header";
const API_URL = "/gateway/Playlists";
class PlaylistService {
  getAllPlaylists() {
    return axios.get(API_URL, { headers: authHeader() });
  }
  getPlaylistContent(playlistId) {
    return axios.get(API_URL + `/${playlistId}`, { headers: authHeader() });
  }
}
export default new PlaylistService();

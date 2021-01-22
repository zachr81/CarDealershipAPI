import http from "./HttpCommon";
class CarsDataService {
	getAll() {
		return http.get("/");
	}
	search(data, matchAll) {
		return http.post(`/search?matchAll=${matchAll}`, data);
	}
}
export default new CarsDataService();
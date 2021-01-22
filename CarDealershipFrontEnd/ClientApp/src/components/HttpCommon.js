import axios from "axios";
export default axios.create({
	baseURL: "http://localhost:57040/cars",
	headers: {
		"Content-type": "application/json",
		"Access-Control-Allow-Origin": "*"
	}
});
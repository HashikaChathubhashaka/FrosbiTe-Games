import axios from 'axios';

const BASE_URL = 'http://localhost:8080';

export async function getToken() {
  try {
    const response = await axios.post(
      `${BASE_URL}/token`,
      {},
      {
        headers: {
          Authorization: 'Basic dXNlcjpwYXNzd29yZA==' // This is the base64-encoded string "user:password"
        }
      }
    );
    return response.data;
  } catch (error) {
    console.error('Error fetching token:', error);
    throw error;
  }
}

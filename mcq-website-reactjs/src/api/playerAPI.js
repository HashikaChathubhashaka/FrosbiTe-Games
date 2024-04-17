import axios from 'axios';

export async function login() {
  try {
    const response = await axios.post('http://20.15.114.131:8080/api/login', {
      apiKey: 'NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI3OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhZA'
    });
    return response.data.token;
  } catch (error) {
    console.error('Error:', error);
    throw error; // Rethrow the error to handle it in the component
  }
}

export async function getUserProfile(JWTtoken) {
  try {
    const response = await axios.get('http://20.15.114.131:8080/api/user/profile/view', {
      headers: {
        Authorization: `Bearer ${JWTtoken}`
      }
    });
    return response.data.user.username;
  } catch (error) {
    console.error('Error:', error);
    throw error; // Rethrow the error to handle it in the component
  }
}

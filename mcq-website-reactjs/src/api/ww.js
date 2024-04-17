const [apikey, setapikey] = useState(""); 
  const [playername,setplayername] = useState("");

  
  useEffect(() => {
    const postData = async () => {
      try {
        const response = await axios.post('http://20.15.114.131:8080/api/login', {
          apiKey: 'NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI3OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhZA'
        });
        setapikey(response.data.token)
        console.log('Response:', response.data.token);
      } catch (error) {
        console.error('Error:', error);
      }
    };

    postData();
  }, []);

  useEffect(() => {
    const getProfile = async () => {
      try {

        const response = await axios.get('http://20.15.114.131:8080/api/user/profile/view', {
          headers: {
            Authorization: `Bearer ${apikey}` // Adding "Bearer" prefix to the token
          }
        });
        setplayername (response.data.user.username)
        console.log('Profile Response:', response.data.user.username);
      } catch (error) {
        console.error('Error:', error);
      }
    };

    if (apikey) {
      getProfile();
    }
  }, [apikey]); // Add apikey as a dependency



  useEffect(() => {
    const getPlayers = async () => {
      try {
        const response = await axios.get('http://localhost:8080/user/players', {
          headers: {
            Authorization: `Bearer ${yourApiKey}`
          }
        });
        // Handle the response data as needed
        console.log('Players:', response.data);
      } catch (error) {
        console.error('Error:', error);
      }
    };
  
    // Make sure your API key is defined
    const yourApiKey = 'eyJhbGciOiJSUzI1NiJ9.eyJpc3MiOiJzZWxmIiwic3ViIjoidXNlciIsImV4cCI6MTcxMzM3NDc1OSwiaWF0IjoxNzEzMzcxMTU5LCJzY29wZSI6InJlYWQgd3JpdGUifQ.BsZ6XNn41bjROQle2fRriINLIL0pRHkv5zeZDuzPlU1kKz4hoa3bX8qY9KLuZhu3HmAjkTP45hEedLxQx5EzkdiRVm57iZYIZUCk78EJVudvtfPphGav3m-I869thi43f8_c2yRQFoqGcz6sbYbdC0RNQftTB_Dfcg9U8K4CloGRqHyjFUvEutMFNZ5_oktERb9YQYt9xdSf3ZHj948K5MIBvc4dk59x9SDJBpL2hTADSRzXBaC8gvxjHSwjpIf4-s-WVJW2k-dDpW3RVJIkzA3OaQMxThO6s5K98PlGPe6WanjTyh7jFotFn14pZRTXE6YM7JDcJ6RyTA4vrSQktQ';
  
    // Call the function only if the API key is provided
    if (yourApiKey) {
      getPlayers();
    }
  }, []); // Empty dependency array to run the effect only once
  
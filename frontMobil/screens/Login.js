import React, {useState} from 'react';
import {
  Dimensions,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
//import auth from '@react-native-firebase/auth';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  /*const onLogin = () => {
    console.log('Login');
    if (!email.trim() || !password.trim()) {
      console.log('Enter email and password');
      return;
    }
    auth()
      .signInWithEmailAndPassword(email, password)
      .then(() => {
        console.log('signed in!');
      })
      .catch(error => {
        console.error(error);
      });
  };
*/
  return (
    <View style={styles.container}>
      <TextInput
        style={styles.inputStyle}
        onChangeText={data => setEmail(data)}
        placeholder="Enter Email"
        placeholderTextColor="#8b9cb5"
        autoCapitalize="none"
        keyboardType="email-address"
        returnKeyType="next"
        underlineColorAndroid="#f000"
        blurOnSubmit={false}
      />
      <TextInput
        style={styles.inputStyle}
        onChangeText={data => setPassword(data)}
        placeholder="Enter password"
        placeholderTextColor="#8b9cb5"
        autoCapitalize="none"
        keyboardType="email-address"
        returnKeyType="next"
        underlineColorAndroid="#f000"
        blurOnSubmit={false}
      />
      <TouchableOpacity style={styles.btnStyle}>
        <Text style={styles.btnTextStyle}>Login</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    display: 'flex',
    justifyContent: 'center',
    height: Dimensions.get('window').height,
  },
  inputStyle: {
    color: 'red',
    paddingLeft: 15,
    paddingRight: 15,
    borderWidth: 1,
    borderRadius: 30,
    borderColor: '#dadae8',
    margin: 10,
  },
  btnStyle: {
    backgroundColor: '#7DE24E',
    borderWidth: 0,
    color: '#FFFFFF',
    borderColor: '#7DE24E',
    height: 40,
    alignItems: 'center',
    borderRadius: 30,
    marginLeft: 35,
    marginRight: 35,
    marginTop: 20,
    marginBottom: 25,
  },
  btnTextStyle: {
    color: '#FFFFFF',
    paddingVertical: 10,
    fontSize: 16,
  },
});

export default Login;
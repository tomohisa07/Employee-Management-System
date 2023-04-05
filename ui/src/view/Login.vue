<template>
    <div id="app" class="container">
      <div class="row">
        <div class="col-md-6 col-md-offset-3">
          <div class="panel panel-default">
            <div class="panel-heading">
              <h3 class="panel-title">Login</h3>
            </div>
            <div class="panel-body">
              <form>
                <div class="form-group">
                  <label for="email">Email</label>
                  <input type="email" class="form-control" id="email" v-model="Email">
                </div>
                <div class="form-group">
                  <label for="password">Password</label>
                  <input type="password" class="form-control" id="password" v-model="Password">
                  <label v-if="error" style="color: red">failed login</label>
                </div>
                <button type="submit" class="btn btn-primary" v-on:click.prevent="login">Submit</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import axios from "axios"

export default {
    name: 'Login',
    data(){
        return{
            Email: '',
            Password: '',
            error:false
        }
    },
    methods:{
        login: function (){
            axios.post("user",{
                Email:this.Email,
                Password:this.Password
            },{ withCredentials: true })
            .then((res)=>{
                console.log(res)
                this.$store.dispatch('saveToken', res.config.xsrfCookieName)
                this.$router.push('/')
                console.log(this.$store.state.token)
            })
            .catch((e) => {
                console.log(e)
                this.error = true;
            })
        }
    }
}
</script>
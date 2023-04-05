import Vue from 'vue'
import App from './App.vue'
// ルーティングのために追加
import router from './router'
import store from './store'
import Axios from 'axios'

Axios.defaults.baseURL = "https://localhost:7049/api/"

Vue.config.productionTip = false

new Vue({
  router, // ルーティングのために追加
  store,
  render: h => h(App),
}).$mount('#app')

import Vue from "vue"
import Vuex from "vuex"
import createPersistedState from "vuex-persistedstate"

Vue.use(Vuex)
const store = new Vuex.Store({
    state: {
        token: ""
    },
    mutations: {
        saveToken(state, token){
          state.token = token
        }
    },
    actions: {
        saveToken({commit}, token){
          commit('saveToken', token)
        }
    },
    getters: {
        getToken(state){
          return state.token
        }
    },
    plugins: [createPersistedState({
        key: 'EMS',
        paths: ['token'],
        storage: window.sessionStorage
      })]
})

export default store
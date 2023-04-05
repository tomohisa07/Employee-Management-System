import Vue from "vue"
import VueRouter from "vue-router"

// import { createRouter, createWebHistory } from 'vue-router'

//コンポーネント読み込み
import Top from '../view/Top.vue'
import Login from '../view/Login.vue'
import home from '@/components/home.vue'
import employee from '@/components/employee.vue'
import department from '@/components/department.vue'

import Store from '../store/index.js'


Vue.use(VueRouter)

//ルーティング定義
const routes = [
    {
      path: '/',
      component: Top,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/login',
      component: Login
    },
    {
      path: '/home',
      name: 'home',
      component: home,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/employee',
      name: 'employee',
      component: employee,
      meta: {
        requiresAuth: true
      }
    },
    {
        path: '/department',
        name: 'department',
        component: department,
        meta: {
          requiresAuth: true
        }
    }
  ]
  const router = new VueRouter({
    mode: 'history',
    routes
  })

//ログイン判定
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!Store.state.token) {
      // 認証されていない時、ログイン画面へ遷移
      next({
        path: '/login'
      })
    } else {
      next();
    }
  } else {
    next();
  }
});

  export default router
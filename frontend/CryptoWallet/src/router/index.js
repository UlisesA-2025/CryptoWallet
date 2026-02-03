import { createRouter, createWebHistory } from 'vue-router'
import TransactionsView from '../views/TransactionsView.vue'
import PortfolioView from '../views/PortfolioView.vue'


const router = createRouter({
  history: createWebHistory(),  
  routes: [
    {path: '/', redirect: '/transactions'},
    {path: '/transactions', component: TransactionsView},
    {path: '/portfolio', component: PortfolioView},
  ],
})

export default router

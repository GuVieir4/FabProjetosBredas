import { createRouter, createWebHistory } from 'vue-router'
import HomePageView from '@/views/HomepageView.vue'
import KartView from '@/views/KartView.vue'
import AdminDashboardView from '@/views/AdminDashboardView.vue'
import ProductDetailsView from '@/views/ProductDetailsView.vue'
import ErrorView from '@/views/NotFoundErrorView.vue'
import CategoryPageView from '@/views/CategoryPageView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomePageView,
    },
    {
      path: '/kart',
      name: 'kart',
      component: KartView,
    },
    {
      path: '/adminDashboard',
      name: 'adminDashboard',
      component: AdminDashboardView,
    },
    {
      path: '/productDetails',
      name: 'productDetails',
      component: ProductDetailsView,
    },
      {
      path: '/categoryPage',
      name: 'categoryPage',
      component: CategoryPageView,
    },
    {
      path: '/error',
      name: 'error',
      component: ErrorView,
    },
  ],
})

export default router

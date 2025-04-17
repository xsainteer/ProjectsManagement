import { createApp } from 'vue'
import App from './App.vue'

import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import { createRouter, createWebHistory } from 'vue-router'
import ProjectWizard from './components/ProjectWizard.vue'
import TaskCreation from './components/TaskCreation.vue'

const routes = [
    { path: '/task-creation', component: TaskCreation },
    { path: '/wizard', component: ProjectWizard }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

const app = createApp(App)

app.use(router)
app.use(ElementPlus)

app.mount('#app')
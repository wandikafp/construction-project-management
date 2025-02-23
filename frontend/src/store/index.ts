import { createStore } from 'vuex';
import type { Commit } from 'vuex';
import axios from 'axios';
import type { Project } from '../models/Project';
import type { Pagination } from '@/models/Pagination';
import { jwtDecode } from 'jwt-decode';

interface State {
  projects: Project[];
  projectDetail: Project | null;
  projectPagination: Pagination;
  token: string | null;
  creatorId: string | null;
}

export default createStore<State>({
  state: {
    projects: [],
    projectDetail: null,
    projectPagination: {
      pageIndex: 1,
      totalPages: 1,
      pageSize: 10,
      totalCount: 0,
    },
    token: localStorage.getItem('token'),
    creatorId: localStorage.getItem('creatorId'),
  },
  mutations: {
    removeProject(state: State, projectId: number) {
      state.projects = state.projects.filter((p) => p.projectId !== projectId);
    },
    updateProject(state: State, { oldProjectId, newProject }: { oldProjectId: number; newProject: Project }) {
      const index = state.projects.findIndex((p) => p.projectId === oldProjectId);
      state.projects.splice(index, 1, newProject); 
    },
    setProjectDetail(state: State, project: Project) {
      state.projectDetail = project;
    },
    setProjects(state: State, projects: Project[]) {
      state.projects = projects;
    },
    setProjectPagination(state: State, pagination: Pagination) {
      state.projectPagination = pagination;
    },
    setToken(state: State, token: string) {
      state.token = token;
      localStorage.setItem('token', token);
    },
    setCreatorId(state: State, id: string) {
      state.creatorId = id;
      localStorage.setItem('creatorId', id);
    },
    clearAuthData(state: State) {
      state.token = null;
      state.creatorId = null;
      localStorage.removeItem('token');
      localStorage.removeItem('creatorId');
    }
  },
  actions: {
    async addProject({ commit }: { commit: Commit }, { project }: { project: Project }) {
      const response = await axios.post('http://localhost:5050/api/project', project);
      console.log(response.data);
      return response.data;
    },
    async removeProject({ commit }: { commit: Commit }, { projectId }: { projectId: number }) {
      console.log(projectId);
      await axios.delete('http://localhost:5050/api/project/' + projectId);
    },
    async updateProject({ commit }: { commit: Commit }, { project }: { project: Project }) {
      const response = await axios.put('http://localhost:5050/api/project', project);
      console.log(response.data);
    },
    async fetchProjectDetail({ commit }: { commit: Commit }, projectId: number) {
      const response = await axios.get('http://localhost:5050/api/project/' + projectId);
      commit('setProjectDetail', response.data);
    },
    async fetchProjects({ commit }: { commit: Commit }, { pageNumber = 1, pageSize = 10, query = "" } = {}) {
      const response = await axios.get('http://localhost:5050/api/project', {
        params: {
          pageNumber,
          pageSize,
          query
        }
      });
      commit('setProjects', response.data.projects);
      commit('setProjectPagination', response.data);
    },
    async login({ commit }: { commit: Commit }, { email, password }: { email: string; password: string }) {
      const response = await axios.post('http://localhost:5050/api/login', {
        email,
        password,
      });
      commit('setToken', response.data.token);
      const decodedToken = jwtDecode(response.data.token);
      commit('setCreatorId', decodedToken.sub);
    },
    logout({ commit }: { commit: Commit }) {
      commit('clearAuthData');
    }
  },
  getters: {
    getProjects: (state: State) => state.projects,
    getProjectDetail: (state: State) => state.projectDetail,
    getProjectPagination: (state: State) => state.projectPagination,
    getToken: (state: State) => state.token,
    getCreatorId: (state: State) => state.creatorId,
  },
});

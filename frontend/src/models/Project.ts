export interface Project {
  projectId: number;
  projectName: string;
  projectLocation: string;
  stage: string;
  category: string;
  otherCategory: string | null;
  startDate: string;
  projectDetails: string;
  creatorId: string;
  creator: User;
}

export interface User {
  email: string;
}

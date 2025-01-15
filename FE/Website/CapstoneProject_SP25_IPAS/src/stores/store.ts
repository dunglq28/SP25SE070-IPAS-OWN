import { create } from "zustand";

interface SidebarState {
  isExpanded: boolean;
  toggleSidebar: () => void;
  setSidebarState: (state: boolean) => void;
}

// Tạo store Zustand
export const useSidebarStore = create<SidebarState>((set) => ({
  isExpanded: true, // Giá trị mặc định của state
  toggleSidebar: () => set((state) => ({ isExpanded: !state.isExpanded })),
  setSidebarState: (state) => set({ isExpanded: state }), // Cập nhật trạng thái của sidebar
}));

export interface PaginationButtonProps {
  onClick: () => void;
  isDisabled?: boolean;
  hoverStyles: { bg: string; color: string };
  text?: string;
  icon: React.ReactNode;
}

export interface NavigationDotProps {
  totalPages: number;
  currentPage?: number;
  rowsPerPage?: number;
  rowsPerPageOptions: number[];
  onPageChange: (newPage: number) => void;
  onRowsPerPageChange: (rowsPerPage: number) => void;
}

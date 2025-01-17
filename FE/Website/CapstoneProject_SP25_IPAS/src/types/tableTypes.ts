export interface TableColumn<T> {
  header: string;
  field: keyof T;
  width: number;
  isSort?: boolean;
  accessor: (item: T) => React.ReactNode;
  className?: string;
}

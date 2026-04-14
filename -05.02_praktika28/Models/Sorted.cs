namespace praktika28.Models
{
    public class Sorted : Tasks
    {
        public Sorted() { }
        public Sorted(Tasks sortedTask) { 
            this.name = sortedTask.Name;
            this.priority = sortedTask.Priority;
            this.comment = sortedTask.Comment;
            this.dateExecute = sortedTask.DateExecute;
            this.done = sortedTask.Done;
        }
    }
}

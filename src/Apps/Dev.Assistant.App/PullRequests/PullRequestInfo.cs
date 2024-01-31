using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace Dev.Assistant.App.PullRequests;

public class PullRequestInfo
{
    public string ArtifactId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public PullRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public string SourceRefName { get; set; }
    public string TargetRefName { get; set; }

    public List<GitCommitRef> Commits { get; set; }

    public List<string> WorkItemRefs { get; set; }

    public List<string> ReviewerComments { get; set; }
    public int TotalComments { get; set; }

    public string DeveloperId { get; set; }
    public string DeveloperName { get; set; }

    /// <summary>
    /// NICHQ\Username
    /// </summary>
    public string DeveloperUniqueName { get; set; }

    public List<ReviewerInfo> Reviewers { get; set; }

    /// <summary>
    /// this include all remarks about the CR, checking for test data, CM, and IG
    /// </summary>
    public string Remarks { get; set; }

}

public class ReviewerInfo
{
    public string Id { get; set; }
    public string DisplayName { get; set; }

    /// <summary>
    /// NICHQ\Username
    /// </summary>
    public string UniqueName { get; set; }
}
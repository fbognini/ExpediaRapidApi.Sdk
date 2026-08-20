using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Activities.Content;

/// <summary>
/// An activity: the bookable unit, and the only one of the three content levels that can be purchased.
/// </summary>
public class ActivityContent
{
    public string Id { get; set; } = default!;

    public string? Title { get; set; }

    /// <summary>
    /// A short, marketing-oriented description.
    /// </summary>
    public string? Tagline { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// The categories this activity belongs to. Resolve them against the activity categories reference: the tree
    /// they form is what drives the category drill-down.
    /// </summary>
    public List<string> CategoryIds { get; set; } = [];

    public List<string> AttributeIds { get; set; } = [];

    public ActivityMediaCollection? Media { get; set; }

    /// <summary>
    /// Where the activity starts. Good enough to place it on a map or in an area.
    /// </summary>
    public List<ActivityLocation> Location { get; set; } = [];

    public ActivityRatings? Ratings { get; set; }

    /// <summary>
    /// What the price includes.
    /// </summary>
    public List<string> Inclusions { get; set; } = [];

    /// <summary>
    /// What the price does not include.
    /// </summary>
    public List<string> Exclusions { get; set; } = [];

    public List<string> KnowBeforeYouGo { get; set; } = [];

    public List<string> KnowBeforeYouBook { get; set; } = [];

    public string? RedemptionInstructions { get; set; }

    /// <summary>
    /// How long the activity lasts, as an ISO-8601 duration. E.g. "PT1H18M".
    /// </summary>
    public string? Duration { get; set; }

    public bool? Cancellable { get; set; }

    public ActivityCancellationPolicy? CancellationPolicy { get; set; }

    public List<ActivitySupplier> Suppliers { get; set; } = [];

    /// <summary>
    /// The booking question data types this activity may ask for at booking time.
    /// A subset of what we declared in supported_booking_data_types, since that is what the content call filters on.
    /// </summary>
    public List<string> BookingQuestionTypes { get; set; } = [];
}

public class ActivityRatings
{
    public SupplierRating? Supplier { get; set; }

    public GuestRating? Guest { get; set; }
}

public class SupplierRating
{
    /// <summary>
    /// Average rating of the supplier across all of their activities, between 1.0 and 5.0.
    /// </summary>
    public string? Rating { get; set; }
}

public class GuestRating
{
    /// <summary>
    /// How many guest reviews this rating averages.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// The overall rating, between 1.0 and 5.0.
    /// </summary>
    public string? Overall { get; set; }

    /// <summary>
    /// How good the activity was for the money, between 1.0 and 5.0.
    /// </summary>
    public string? Value { get; set; }
}

public class ActivityCancellationPolicy
{
    public ActivityCancellationPolicyType? Type { get; set; }

    /// <summary>
    /// How many hours before the start time (or start date, depending on <see cref="Type"/>) the booking can still
    /// be cancelled without penalty. Not applicable when the activity is non-cancellable.
    /// </summary>
    public int? Hours { get; set; }
}

public enum ActivityCancellationPolicyType
{
    before_start_time,
    before_start_date,
    noncancellable
}

public class ActivitySupplier
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public ActivityAddress? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
}

/// <summary>
/// A category or an attribute, of either an activity or an experience.
/// </summary>
/// <remarks>
/// These form a tree, but by name and not by id: the <see cref="Type"/> of a node is the <see cref="Name"/> of its
/// parent. E.g. the activity category { name: "Bungy Jumping", type: "Outdoor Activities" } hangs off the experience
/// category { name: "Outdoor Activities", type: "Adventure" }. Match on the strings, and expect orphans.
/// </remarks>
public class ActivityReference
{
    public string Id { get; set; } = default!;

    /// <summary>
    /// The display name of this node.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// The name of the parent node. NOT an id.
    /// </summary>
    public string? Type { get; set; }
}

/// <summary>
/// The opening hours of an activity on a given day, area by area.
/// </summary>
public class ActivityOperatingHours
{
    public DateOnly Date { get; set; }

    public List<ActivityOperatingArea> Areas { get; set; } = [];
}

public class ActivityOperatingArea
{
    /// <summary>
    /// The area within the experience. E.g. "Gardens", "Museum".
    /// </summary>
    public string? AreaName { get; set; }

    /// <summary>
    /// Opening time, local, as HH:MM.
    /// </summary>
    public string? Open { get; set; }

    /// <summary>
    /// Closing time, local, as HH:MM.
    /// </summary>
    public string? Close { get; set; }

    /// <summary>
    /// Last admission time, local, as HH:MM.
    /// </summary>
    public string? LastAdmission { get; set; }
}

public class GuestReview
{
    /// <summary>
    /// Language of the review, in ISO 639-1 format.
    /// </summary>
    public string? Language { get; set; }

    public string? ReviewerName { get; set; }

    /// <summary>
    /// The rating given by the reviewer, between 1.0 and 5.0.
    /// </summary>
    public string? Rating { get; set; }

    public string? Text { get; set; }

    public DateOnly? DateSubmitted { get; set; }

    /// <summary>
    /// Where the review comes from. E.g. "Tiqets", "Expedia Verified Guest".
    /// </summary>
    public string? Source { get; set; }
}

/// <summary>
/// An activity group: several activities the supplier sells as variants of the same thing.
/// </summary>
public class ActivityGroupContent
{
    public string Id { get; set; } = default!;

    public string? Title { get; set; }

    public string? Tagline { get; set; }

    public string? Description { get; set; }

    public ActivityMediaCollection? Media { get; set; }

    public List<ActivityLocation> Location { get; set; } = [];

    public StarRating? Rating { get; set; }

    public List<string> CategoryIds { get; set; } = [];

    public List<string> AttributeIds { get; set; } = [];

    /// <summary>
    /// The activities in this group.
    /// </summary>
    public List<string> ActivityIds { get; set; } = [];

    /// <summary>
    /// The booking question data types the activities of this group may ask for at booking time.
    /// </summary>
    public List<string> BookingQuestionTypes { get; set; } = [];
}

/// <summary>
/// An experience: a marketing wrapper over activities and activity groups. Not bookable in itself.
/// </summary>
public class ExperienceContent
{
    public string Id { get; set; } = default!;

    public string? Title { get; set; }

    public string? Tagline { get; set; }

    public string? Description { get; set; }

    public ActivityMediaCollection? Media { get; set; }

    public List<ActivityLocation> Location { get; set; } = [];

    public StarRating? Rating { get; set; }

    public List<string> CategoryIds { get; set; } = [];

    public List<string> AttributeIds { get; set; } = [];

    public List<string> ActivityIds { get; set; } = [];

    public List<string> ActivityGroupIds { get; set; } = [];

    /// <summary>
    /// The booking question data types the activities of this experience may ask for at booking time.
    /// </summary>
    public List<string> BookingQuestionTypes { get; set; } = [];
}

namespace Api.Domain;

public enum MediaKind { Movie = 1, Episode = 2 }
public enum PersonRole { Cast = 1, Director = 2, Writer = 3, Producer = 4 }
public enum ContainerFormat { Fmp4 = 1, Ts = 2 }
public enum TranscodeStatus { Pending = 1, InProgress = 2, Ready = 3, Failed = 4 }
public enum PlaybackType { DirectPlay = 1, Remux = 2, Transcode = 3 }
